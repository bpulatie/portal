<?xml version="1.0" encoding="utf-8"?>

<xsl:stylesheet version="2.0"
    xmlns:xsl="http://www.w3.org/1999/XSL/Transform" xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance">

  <xsl:output method="html" encoding="utf-8" indent="yes" />
<xsl:template match="/">


 <html lang="en">
		<head>
      <title>RDM Documents Report</title>
      <meta name="viewport" content="width=device-width, initial-scale=1.0" />
      <meta http-equiv="Content-Type" content="text/htm; charset=utf-8" />
      <meta http-equiv="X-UA-Compatible" content="IE=edge"/>      

      <!-- Latest compiled and minified CSS -->
      <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/css/bootstrap.min.css" integrity="sha384-BVYiiSIFeK1dGmJRAkycuHAHRg32OmUcww7on3RYdg4Va+PmSTsz/K68vbdEjh4u" crossorigin="anonymous"/>
      <!-- Latest compiled and minified JavaScript -->
      <script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/js/bootstrap.min.js" integrity="sha384-Tc5IQib027qvyjSMfHjOMaLkfuWVxZxUPnCJA7l2mCWNIpG9mGCD8wGNIcPD7Txa" crossorigin="anonymous"></script>
      <link rel="shortcut icon" href="../favicon.ico" type="image/x-icon" />
      <link rel="icon" href="../favicon.ico" type="image/x-icon" />

      <!-- HTML5 Shim and Respond.js IE8 support of HTML5 elements and media queries -->
      <!-- WARNING: Respond.js doesn't work if you view the page via file:// -->
      <!--[if lt IE 9]>
        <script src="https://oss.maxcdn.com/libs/html5shiv/3.7.0/html5shiv.js"></script>
        <script src="https://oss.maxcdn.com/libs/respond.js/1.3.0/respond.min.js"></script>
    <![endif]-->
      
      <style>
        @page           { size:  auto;  margin: 0mm; }
        .under          { border-bottom: 3px solid #cccccc; }
      </style>
		</head>
		<body>
      <br/>
      <div class="container">

        <xsl:for-each select="NewDataSet/Table">
            <div class="row under">
              <h3>
                <xsl:value-of select="./name"/>
                <xsl:text> </xsl:text>
                <xsl:if test="./ref_no!=''">
                  <small>
                    <xsl:value-of select="./ref_no"/>
                  </small>
                </xsl:if>
              </h3>
            </div>

            <div class="row">
              <p>
                <xsl:value-of select="./detail" disable-output-escaping="yes"/>
              </p>
            </div>
        </xsl:for-each>
      </div>
   </body>
</html>
</xsl:template>

</xsl:stylesheet>

<!--

		<div style="margin:10px;padding-left:40px;text-align:left">
				<xsl:for-each select="NewDataSet/Table">
					<p>
						<div>
							<xsl:attribute name="style">font-size:12px;font-weight:bold;padding-left:<xsl:value-of select="./level_id * 20"/>px</xsl:attribute>
							<xsl:if test="./reference!=''">
								<xsl:value-of select="./reference"/>:
							</xsl:if>
							<xsl:value-of select="./name"/>
						</div>
						<div>
							<xsl:attribute name="style">font-size:10px;padding-left:<xsl:value-of select="./level_id * 20"/>px</xsl:attribute>
							<xsl:value-of select="./detail"/>
						</div>
					</p>
					</xsl:for-each>
			</div>


-->