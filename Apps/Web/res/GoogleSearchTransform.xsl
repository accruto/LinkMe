<?xml version="1.0" encoding="utf-8"?>

<xsl:stylesheet version="1.0"
    xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
  <xsl:output omit-xml-declaration="yes" media-type="text/html" indent="yes"/>
  <xsl:template match="/">
    <xsl:for-each select="GSP/RES/R">
      <div id="featured-item">
        <h1>
          <xsl:value-of select="T" />
        </h1>
        <p>
          <xsl:value-of select="S" />
        </p>
        <div id="link">
          <a>
            <xsl:attribute name="href">
              <xsl:value-of select="U" />
            </xsl:attribute>
            
            Read more
          </a>
        </div>
      </div>
    </xsl:for-each>
  </xsl:template>
</xsl:stylesheet>

